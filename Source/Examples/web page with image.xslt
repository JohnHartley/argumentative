<?xml version="1.0" encoding="UTF-8"?>
<!-- This stylesheet generates a web page with an image.  The image has an links to take you to the text of the element -->
<!-- Version 1.1  References added to h3 titles-->

<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:argImage="urn:image" version="1.0">
  <xsl:output method="html" doctype-system="http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd" doctype-public="-//W3C//DTD XHTML 1.0 Transitional//EN"/>
  <xsl:template match="/">
    <html>
      <head>
        <title>
          <xsl:text>Argument - </xsl:text>
          <xsl:value-of select="/argument/premise/title"/>
        </title>
      </head>
      <body>
        <!-- The whole argument Image is inserted here -->
        <img usemap="#map">
          <xsl:attribute name="src">
            <!-- The ImageToPNG function takes two arguments, the reference for the element and the file name to generate -->
            <xsl:value-of select="argImage:ImageToPNG('','argument.png')"/>
          </xsl:attribute>
        </img>
        <xsl:apply-templates select="argument"/>
        <map name="map">
          <xsl:apply-templates select="argument/premise" mode="map"/>
        </map>
    	<div style="height:100%"/>
      </body>
    </html>
  </xsl:template>
  <xsl:template match="premise">
    <h2>Main Premise</h2>
    <p>
      <a name="{ref}"/>
      <xsl:value-of select="title"/>
    </p>
  	<p><xsl:value-of select="comment"/></p>
    <xsl:apply-templates select="reason|objection|helper"/>
  </xsl:template>
  <xsl:template match="reason | objection | helper">
    <a name="{ref}"/>
    <h3>
      <xsl:value-of select="ref"/>
      <xsl:text>: </xsl:text>
      <xsl:value-of select="name()"/>
    </h3>
    <p>
      <xsl:value-of select="title"/>
    </p>
  	<p><xsl:value-of select="comment"/></p>
    <xsl:apply-templates select="reason|objection|helper"/>
  </xsl:template>
  <xsl:template match="reason | objection | helper" mode="map">
    <area shape="rect">
      <xsl:attribute name="coords">
        <xsl:value-of select="x"/>
        <xsl:text>,</xsl:text>
        <xsl:value-of select="y"/>
        <xsl:text>,</xsl:text>
        <xsl:value-of select="number(width)+number(x)"/>
        <xsl:text>,</xsl:text>
        <xsl:value-of select="number(height)+number(y)"/>
        <xsl:text>,</xsl:text>
      </xsl:attribute>
      <xsl:attribute name="href">
        <xsl:text>#</xsl:text>
        <xsl:value-of select="ref"/>
      </xsl:attribute>
      <xsl:attribute name="alt">
        <xsl:value-of select="title"/>
      </xsl:attribute>
    </area>
    <xsl:apply-templates select="reason|objection|helper" mode="map"/>
  </xsl:template>
  <xsl:template match="text()" mode="map"/>
  <xsl:template match="text()"/>
</xsl:stylesheet>
