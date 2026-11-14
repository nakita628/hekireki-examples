defmodule LangWatch.CustomRole do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: LangWatch.PrismaDateTime, autogenerate: {LangWatch.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          name: String.t(),
          description: String.t() | nil,
          permissions: map(),
          kind: String.t(),
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          organization: LangWatch.Organization.t() | nil,
          assigned_users: [LangWatch.TeamUser.t()],
          role_bindings: [LangWatch.RoleBinding.t()]
        }

  schema "CustomRole" do
    field(:id, :string, primary_key: true)
    field(:name, :string)
    field(:description, :string)
    field(:permissions, :map)
    field(:kind, :string, default: "custom")
    field(:organization_id, :string, source: :organizationId)
    belongs_to(:organization, LangWatch.Organization, foreign_key: :organization_id, define_field: false, type: :string)
    has_many(:assigned_users, LangWatch.TeamUser, foreign_key: :assigned_role_id, references: :id)
    has_many(:role_bindings, LangWatch.RoleBinding, foreign_key: :custom_role_id, references: :id)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end