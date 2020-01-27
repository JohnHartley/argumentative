using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;

namespace Argumentative.tests
{
    /// <summary>
    /// Test Node operations
    /// </summary>
    [TestFixture]
    class TestMetadata
    {
        private string examplesPath = "..\\..\\examples\\";

        [Test]
        public void testMetadata()
        {
            MetadataList ml = new MetadataList(examplesPath);
            Assert.AreEqual(14, ml.Files.Length);
        }

        [Test]
        public void testMetadataPaths()
        {
            MetadataList ml = new MetadataList("c:\\windows");
            Assert.AreEqual(0, ml.Files.Length);
            // Non existant file
            ml = new MetadataList("c:\\xyz");
            Assert.AreEqual(0, ml.Files.Length);

        }
        
        [Test]
        public void testProcessMetadata()
        {
            MetadataList ml = new MetadataList(examplesPath);
            Metadata m = ml.processFile(examplesPath + "graphviz.xslt");
            Assert.AreEqual("Graphviz", m.Get(Metadata.titleTag));
            Assert.IsNotNullOrEmpty(m.Get(Metadata.descriptionTag));
            Assert.IsTrue(m.Get(Metadata.descriptionTag).IndexOf("Graphviz") != -1);
        }

        [Test]
        public void testMetadataTitle()
        {
            MetadataList ml = new MetadataList(examplesPath);
            Metadata m = ml.processFile(examplesPath + "text.xslt");
            Assert.IsNull(m.Get(Metadata.titleTag));
            string title = m.getTitle();
            Assert.AreEqual("text.xslt",title);
        }

        [Test]
        public void testMetadataProcess()
        {
            MetadataList ml = new MetadataList(examplesPath);
            Assert.AreEqual(14, ml.Files.Length);
            ml.processMetadataList();
            Assert.AreEqual(14, ml.List.Count);
        }

        [Test]
        public void loadListBox()
        {
            System.Windows.Forms.ListBox listbox = new System.Windows.Forms.ListBox();
            listbox.Items.Add("Item 1");
            Assert.AreEqual(1, listbox.Items.Count);
            MetadataList metadatalist = new MetadataList(examplesPath);
            metadatalist.processMetadataList();
            foreach(Metadata metadata in metadatalist.List)
                listbox.Items.Add(metadata.getTitle());
            Assert.AreEqual(15, listbox.Items.Count);  // 14 files plus first item
        }
    }
}
