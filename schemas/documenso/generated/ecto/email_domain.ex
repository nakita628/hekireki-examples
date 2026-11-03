defmodule Documenso.EmailDomain do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Documenso.PrismaDateTime, autogenerate: {Documenso.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          status: atom(),
          selector: String.t(),
          domain: String.t(),
          public_key: String.t(),
          private_key: String.t(),
          last_verified_at: DateTime.t() | nil,
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          organisation: Documenso.Organisation.t() | nil,
          emails: [Documenso.OrganisationEmail.t()]
        }

  schema "EmailDomain" do
    field(:id, :string, primary_key: true)
    field(:status, Ecto.Enum, values: [:PENDING, :ACTIVE], default: :PENDING)
    field(:selector, :string)
    field(:domain, :string)
    field(:public_key, :string, source: :publicKey)
    field(:private_key, :string, source: :privateKey)
    field(:last_verified_at, Documenso.PrismaDateTime, source: :lastVerifiedAt)
    field(:organisation_id, :string, source: :organisationId)
    belongs_to(:organisation, Documenso.Organisation, foreign_key: :organisation_id, define_field: false, type: :string)
    has_many(:emails, Documenso.OrganisationEmail, foreign_key: :email_domain_id)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end