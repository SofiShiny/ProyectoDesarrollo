using Eventos.Dominio.ObjetosDeValor;
using FluentAssertions;
using Xunit;

namespace Eventos.Pruebas.Dominio;

public class UbicacionTests
{
    [Fact]
    public void CrearUbicacion_ConDatosValidos_DeberiaExitoso()
    {
        // Preparar
        var nombreLugar = "Calle7";
        var direccion = "El Marques";
        var ciudad = "Caracas";
        var region = "DF";
        var codigoPostal = "1073";
        var pais = "Venezuela";

        // Actuar
        var ubicacion = new Ubicacion(nombreLugar, direccion, ciudad, region, codigoPostal, pais);

        // Comprobar
        ubicacion.Should().NotBeNull();
        ubicacion.NombreLugar.Should().Be(nombreLugar);
        ubicacion.Direccion.Should().Be(direccion);
        ubicacion.Ciudad.Should().Be(ciudad);
        ubicacion.Region.Should().Be(region);
        ubicacion.CodigoPostal.Should().Be(codigoPostal);
        ubicacion.Pais.Should().Be(pais);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    public void CrearUbicacion_ConNombreLugarInvalido_DeberiaLanzarExcepcion(string nombreLugar)
    {
        // Actuar
        Action act = () => new Ubicacion(nombreLugar!, "El Marques", "Caracas", "DF", "1073", "Venezuela");

        // Comprobar
        act.Should().Throw<ArgumentException>()
            .WithMessage("*nombreLugar*");
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    public void CrearUbicacion_ConCiudadInvalida_DeberiaLanzarExcepcion(string ciudad)
    {
        // Actuar
        Action act = () => new Ubicacion("Calle7", "El Marques", ciudad!, "DF", "1073", "Venezuela");

        // Comprobar
        act.Should().Throw<ArgumentException>()
            .WithMessage("*ciudad*");
    }

    [Fact]
    public void Equals_ConMismosValores_DeberiaRetornarTrue()
    {
        // Preparar
        var ubicacion1 = new Ubicacion("Calle7", "El Marques", "Caracas", "DF", "1073", "Venezuela");
        var ubicacion2 = new Ubicacion("Calle7", "El Marques", "Caracas", "DF", "1073", "Venezuela");

        // Actuar
        var sonIguales = ubicacion1.Equals(ubicacion2);

        // Comprobar
        sonIguales.Should().BeTrue();
    }

    [Fact]
    public void Equals_ConValoresDiferentes_DeberiaRetornarFalse()
    {
        // Preparar
        var ubicacion1 = new Ubicacion("Calle7", "El Marques", "Caracas", "DF", "1073", "Venezuela");
        var ubicacion2 = new Ubicacion("Centro Cultural", "Av Principal", "Maracay", "AR", "2101", "Venezuela");

        // Actuar
        var sonIguales = ubicacion1.Equals(ubicacion2);

        // Comprobar
        sonIguales.Should().BeFalse();
    }

    [Fact]
    public void GetHashCode_ConMismosValores_DeberiaRetornarMismoHash()
    {
        // Preparar
        var ubicacion1 = new Ubicacion("Calle7", "El Marques", "Caracas", "DF", "1073", "Venezuela");
        var ubicacion2 = new Ubicacion("Calle7", "El Marques", "Caracas", "DF", "1073", "Venezuela");

        // Actuar
        var hash1 = ubicacion1.GetHashCode();
        var hash2 = ubicacion2.GetHashCode();

        // Comprobar
        hash1.Should().Be(hash2);
    }
}