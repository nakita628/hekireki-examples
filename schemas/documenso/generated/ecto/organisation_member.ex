defmodule Documenso.OrganisationMember do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Documenso.PrismaDateTime, autogenerate: {Documenso.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          user: Documenso.User.t() | nil,
          organisation: Documenso.Organisation.t() | nil,
          organisation_group_members: [Documenso.OrganisationGroupMember.t()]
        }

  schema "OrganisationMember" do
    field(:id, :string, primary_key: true)
    field(:user_id, :id, source: :userId)
    field(:organisation_id, :string, source: :organisationId)
    belongs_to(:user, Documenso.User, foreign_key: :user_id, define_field: false)
    belongs_to(:organisation, Documenso.Organisation, foreign_key: :organisation_id, define_field: false, type: :string)
    has_many(:organisation_group_members, Documenso.OrganisationGroupMember, foreign_key: :organisation_member_id, references: :id)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end