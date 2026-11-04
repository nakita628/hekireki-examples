defmodule Documenso.OrganisationAuthenticationPortal do
  use Ecto.Schema

  @primary_key false

  @type t :: %__MODULE__{
          id: String.t(),
          enabled: boolean(),
          client_id: String.t(),
          client_secret: String.t(),
          well_known_url: String.t(),
          default_organisation_role: atom(),
          auto_provision_users: boolean(),
          allowed_domains: [String.t()],
          allow_personal_organisations: boolean(),
          organisation: Documenso.Organisation.t() | nil
        }

  schema "OrganisationAuthenticationPortal" do
    field(:id, :string, primary_key: true)
    field(:enabled, :boolean, default: false)
    field(:client_id, :string, default: "", source: :clientId)
    field(:client_secret, :string, default: "", source: :clientSecret)
    field(:well_known_url, :string, default: "", source: :wellKnownUrl)
    field(:default_organisation_role, Ecto.Enum, values: [:ADMIN, :MANAGER, :MEMBER], default: :MEMBER, source: :defaultOrganisationRole)
    field(:auto_provision_users, :boolean, default: true, source: :autoProvisionUsers)
    field(:allowed_domains, {:array, :string}, source: :allowedDomains)
    field(:allow_personal_organisations, :boolean, default: false, source: :allowPersonalOrganisations)
    has_one(:organisation, Documenso.Organisation, foreign_key: :organisation_authentication_portal_id, references: :id)
  end
end