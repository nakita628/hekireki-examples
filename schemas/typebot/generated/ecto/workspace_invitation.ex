defmodule Typebot.WorkspaceInvitation do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Typebot.PrismaDateTime, autogenerate: {Typebot.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          email: String.t(),
          type: atom(),
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          workspace: Typebot.Workspace.t() | nil
        }

  schema "WorkspaceInvitation" do
    field(:id, :string, primary_key: true)
    field(:email, :string)
    field(:type, Ecto.Enum, values: [:ADMIN, :MEMBER, :GUEST])
    field(:workspace_id, :string, source: :workspaceId)
    belongs_to(:workspace, Typebot.Workspace, foreign_key: :workspace_id, define_field: false, type: :string)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end