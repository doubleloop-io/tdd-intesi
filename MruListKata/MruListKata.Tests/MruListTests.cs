using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace MruListKata.Tests
{
    public class MruListTests
    {
        [Fact]
        public void TrackOneFile()
        {
            var mru = new MruList();

            mru.Track("f1");
            var result = mru.RecentlyTracked;

            Assert.Equal(new[] {"f1"}, result);
        }

        [Fact]
        public void NoTrackedFiles()
        {
            var mru = new MruList();

            var result = mru.RecentlyTracked;

            Assert.Empty(result);
        }

        [Fact]
        public void TrackManyFiles()
        {
            var mru = new MruList();

            mru.Track("f1", "f2", "f3");
            var result = mru.RecentlyTracked;

            Assert.Equal(new[] {"f3", "f2", "f1"}, result);
        }

        [Fact]
        public void TrackDuplicatedFiles()
        {
            var mru = new MruList();

            mru.Track("f1", "f2", "f3", "f2");
            var result = mru.RecentlyTracked;

            Assert.Equal(new[] {"f2", "f3", "f1"}, result);
        }

        [Theory]
        [InlineData("f2", "f3", "f4")]
        [InlineData("f1", "f2", "f3", "f4")]
        public void TrackManyFilesOverCapacity(params string[] files)
        {
            var capacity = 2;
            var mru = new MruList(capacity);

            mru.Track(files);
            var result = mru.RecentlyTracked;

            Assert.Equal(new[] {"f4", "f3"}, result);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void TrackInvalidFile(string value)
        {
            var mru = new MruList();

            var ex = Record.Exception(() => mru.Track(value));

            Assert.IsType<ArgumentException>(ex);
        }

        [Fact]
        public void TrackNullFiles()
        {
            var mru = new MruList();

            var ex = Record.Exception(() => mru.Track(null));

            Assert.IsType<ArgumentNullException>(ex);
        }
    }

    public class MruList
    {
        const int DEFAULT_CAPACITY = 20;

        readonly int capacity;
        readonly List<string> files;

        // Safe Refactoring Rules
        // espandere/parallela -> migrare -> rimuovere il vecchio
        public MruList(int capacity = DEFAULT_CAPACITY)
        {
            this.capacity = capacity;
            files = new List<string>();
        }

        public void Track(params string[] newFiles)
        {
            if (newFiles == null)
                throw new ArgumentNullException(nameof(newFiles));
            if (newFiles.Any(String.IsNullOrWhiteSpace))
                throw new ArgumentException(nameof(newFiles));

            foreach (var newFile in newFiles)
            {
                files.Remove(newFile);
                files.Insert(0, newFile);
            }

            if (files.Count > capacity)
                files.RemoveRange(capacity, files.Count - capacity);
        }

        public IEnumerable<string> RecentlyTracked => files;
    }
}
