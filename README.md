# BANOBRAS - Backend del Sistema SICOFIN

[![Codacy Badge](https://app.codacy.com/project/badge/Grade/dc6199c4794948059580c7978e17dbdd)](https://www.codacy.com/gh/Ontica/Banobras.Sicofin/dashboard?utm_source=github.com&amp;utm_medium=referral&amp;utm_content=Ontica/Banobras.Sicofin&amp;utm_campaign=Badge_Grade)
&nbsp; &nbsp;
[![Maintainability](https://api.codeclimate.com/v1/badges/a55ffb3c78e78c77e09b/maintainability)](https://codeclimate.com/github/Ontica/Banobras.Sicofin/maintainability)

El Sistema de Contabilidad Financiera (SICOFIN) está siendo desarrollado y evolucionado
por nuestra organización, a la medida de las necesidades del Banco Nacional de Obras y
Servicios Públicos, S.N.C. (BANOBRAS).

[BANOBRAS](https://www.gob.mx/banobras) es una institución de banca de desarrollo mexicana cuya labor
es financiar obras para la creación de servicios públicos. Por el tamaño de su cartera de crédito directo,
es el cuarto Banco más grande del sistema bancario mexicano y el primero de la Banca de Desarrollo.

SICOFIN está basado en [Empiria Financial Accounting](https://github.com/Ontica/Empiria.FinancialAccounting).

Este *backend* está siendo desarrollado en C# 7.0, .NET Framework 4.8 y ASP .NET, y utiliza
diversos componentes de [Empiria Framework](https://github.com/Ontica/Empiria.Core)
y [Empiria Extensions](https://github.com/Ontica/Empiria.Extensions).

Este repositorio aglutina todos los componentes que conforman el *backend* del Sistema SICOFIN,
y contiene código específico que permite su individualización a las necesidades actuales
de BANOBRAS.

## Contenido

1.  **External Interfaces**  
    Casos de uso y servicios específicos para integrar el Sistema SICOFIN
    con otros sistemas de BANOBRAS.  

    Ejemplo de estas integraciones son las importaciones de volantes provenientes
    de otros sistemas, la exportación de saldos y balanzas a otros sistemas, así
    como la invocación de procesos externos.

2.  **Web Api**  
    A través de estos servicios web es que se comunica el *backend* de SICOFIN
    con otros sistemas, incluyendo la aplicación *frontend* del propio Sistema.

    Además, sirve como integrador de todos los módulos con servicios web
    necesarios para la ejecución del *backend* del Sistema.
    
    Puede contener sus propias web apis y también permite sobreescribir el funcionamiento
    de otras web apis, mandando ejecutar casos de uso o servicios distintos a los
    predeterminados.  

    Este módulo es el que se instala en el servidor de aplicaciones IIS donde
    se ejecuta el *backend* del Sistema SICOFIN.

## Licencia

Este producto y sus partes se distribuyen mediante una licencia GNU AFFERO
GENERAL PUBLIC LICENSE, para uso exclusivo de BANOBRAS y de su personal.

Para cualquier otro uso (con excepción a lo estipulado en los Términos de
Servicio de GitHub), es indispensable obtener con nuestra organización una
licencia distinta a esta.

Lo anterior restringe la distribución, copia, modificación, almacenamiento,
instalación, compilación o cualquier otro uso del producto o de sus partes,
a terceros, empresas privadas o a su personal, sean o no proveedores de
servicios de las entidades públicas mencionadas.

El desarrollo, evolución y mantenimiento de este producto está siendo pagado
en su totalidad con recursos públicos, y está protegido por las leyes nacionales
e internacionales de derechos de autor.

## Copyright

Copyright © 2021-2024. La Vía Óntica SC, Ontica LLC y autores.
Todos los derechos reservados.
