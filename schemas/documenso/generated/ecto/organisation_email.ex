defmodule Documenso.OrganisationEmail do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Documenso.PrismaDateTime, autogenerate: {Documenso.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          email: String.t(),
          email_name: String.t(),
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          email_domain: Documenso.EmailDomain.t() | nil,
          organisation: Documenso.Organisation.t() | nil,
          organisation_global_settings: [Documenso.OrganisationGlobalSettings.t()],
          team_global_settings: [Documenso.TeamGlobalSettings.t()]
        }

  schema "OrganisationEmail" do
    field(:id, :string, primary_key: true)
    field(:email, :string)
    field(:email_name, :string, source: :emailName)
    field(:email_domain_id, :string, source: :emailDomainId)
    field(:organisation_id, :string, source: :organisationId)
    belongs_to(:email_domain, Documenso.EmailDomain, foreign_key: :email_domain_id, define_field: false, type: :string)
    belongs_to(:organisation, Documenso.Organisation, foreign_key: :organisation_id, define_field: false, type: :string)
    has_many(:organisation_global_settings, Documenso.OrganisationGlobalSettings, foreign_key: :email_id)
    has_many(:team_global_settings, Documenso.TeamGlobalSettings, foreign_key: :email_id)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end