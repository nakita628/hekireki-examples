defmodule Typebot.MemberInWorkspace do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Typebot.PrismaDateTime, autogenerate: {Typebot.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          user_id: String.t(),
          workspace_id: String.t(),
          role: atom(),
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          user: Typebot.User.t() | nil,
          workspace: Typebot.Workspace.t() | nil
        }

  schema "MemberInWorkspace" do
    field(:role, Ecto.Enum, values: [:ADMIN, :MEMBER, :GUEST])
    field(:user_id, :string, primary_key: true, source: :userId)
    field(:workspace_id, :string, primary_key: true, source: :workspaceId)
    belongs_to(:user, Typebot.User, foreign_key: :user_id, define_field: false, type: :string)
    belongs_to(:workspace, Typebot.Workspace, foreign_key: :workspace_id, define_field: false, type: :string)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end