defmodule LangWatch.GroupMembership do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: LangWatch.PrismaDateTime, autogenerate: {LangWatch.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          user_id: String.t(),
          group_id: String.t(),
          inserted_at: DateTime.t(),
          user: LangWatch.User.t() | nil,
          group: LangWatch.Group.t() | nil
        }

  schema "GroupMembership" do
    field(:user_id, :string, primary_key: true, source: :userId)
    field(:group_id, :string, primary_key: true, source: :groupId)
    belongs_to(:user, LangWatch.User, foreign_key: :user_id, define_field: false, type: :string)
    belongs_to(:group, LangWatch.Group, foreign_key: :group_id, define_field: false, type: :string)
    timestamps(inserted_at_source: :createdAt, updated_at: false)
  end
end