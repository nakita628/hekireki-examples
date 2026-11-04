defmodule Trigger.OrgMember do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Trigger.PrismaDateTime, autogenerate: {Trigger.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          role: atom(),
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          organization: Trigger.Organization.t() | nil,
          user: Trigger.User.t() | nil,
          environments: [Trigger.RuntimeEnvironment.t()]
        }

  schema "OrgMember" do
    field(:id, :string, primary_key: true)
    field(:role, Ecto.Enum, values: [:ADMIN, :MEMBER], default: :MEMBER)
    field(:organization_id, :string, source: :organizationId)
    field(:user_id, :string, source: :userId)
    belongs_to(:organization, Trigger.Organization, foreign_key: :organization_id, define_field: false, type: :string)
    belongs_to(:user, Trigger.User, foreign_key: :user_id, define_field: false, type: :string)
    has_many(:environments, Trigger.RuntimeEnvironment, foreign_key: :org_member_id, references: :id)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end