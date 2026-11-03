defmodule Documenso.OrganisationMemberInvite do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Documenso.PrismaDateTime, autogenerate: {Documenso.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          email: String.t(),
          token: String.t(),
          status: atom(),
          organisation_role: atom(),
          inserted_at: DateTime.t(),
          organisation: Documenso.Organisation.t() | nil
        }

  schema "OrganisationMemberInvite" do
    field(:id, :string, primary_key: true)
    field(:email, :string)
    field(:token, :string)
    field(:status, Ecto.Enum, values: [:ACCEPTED, :PENDING, :DECLINED], default: :PENDING)
    field(:organisation_role, Ecto.Enum, values: [:ADMIN, :MANAGER, :MEMBER], source: :organisationRole)
    field(:organisation_id, :string, source: :organisationId)
    belongs_to(:organisation, Documenso.Organisation, foreign_key: :organisation_id, define_field: false, type: :string)
    timestamps(inserted_at_source: :createdAt, updated_at: false)
  end
end