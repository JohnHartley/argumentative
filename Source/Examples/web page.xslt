<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform" version="1.0">
<xsl:output method="html"/>

  <xsl:template match="/">
  <html>
    <head>
      <title>Argument</title>
    </head>
    <body>
    <h1><xsl:value-of select="//premise/title"/></h1>
    	
  	<p><xsl:value-of select="//premise/comment"/></p>
      <xsl:apply-templates select="*"/>
    </body>
  </html>
  </xsl:template>
  
<xsl:template match="premise">
<div class="premise">
<xsl:apply-templates select="reason|objection|helper"/>
</div>
</xsl:template> 

<xsl:template match="reason|objection|helper">
<div style="margin:6mm;border:thin solid grey;padding-left:6mm">
<strong>
<xsl:value-of select="translate(substring(name(),1,1),'abcdefghijklmnopqrstuvwxyz','ABCDEFGHIJKLMNOPQRSTUVWXYZ')"/>
<xsl:value-of select="substring(name(),2,string-length(name()))"/>
<xsl:text>: </xsl:text>
<xsl:value-of select="title"/>
</strong>
<xsl:apply-templates select="reason|objection|helper"/>
</div>
</xsl:template> 

  <xsl:template match="text()"/>
</xsl:stylesheet>
