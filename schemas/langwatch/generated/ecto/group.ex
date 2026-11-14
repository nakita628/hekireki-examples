defmodule LangWatch.Group do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: LangWatch.PrismaDateTime, autogenerate: {LangWatch.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          name: String.t(),
          slug: String.t(),
          external_id: String.t() | nil,
          scim_source: String.t() | nil,
          scim_connection_id: String.t() | nil,
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          organization: LangWatch.Organization.t() | nil,
          members: [LangWatch.GroupMembership.t()],
          role_bindings: [LangWatch.RoleBinding.t()]
        }

  schema "Group" do
    field(:id, :string, primary_key: true)
    field(:name, :string)
    field(:slug, :string)
    field(:external_id, :string, source: :externalId)
    field(:scim_source, :string, source: :scimSource)
    field(:scim_connection_id, :string, source: :scimConnectionId)
    field(:organization_id, :string, source: :organizationId)
    belongs_to(:organization, LangWatch.Organization, foreign_key: :organization_id, define_field: false, type: :string)
    has_many(:members, LangWatch.GroupMembership, foreign_key: :group_id, references: :id)
    has_many(:role_bindings, LangWatch.RoleBinding, foreign_key: :group_id, references: :id)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end