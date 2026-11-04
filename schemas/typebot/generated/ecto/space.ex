defmodule Typebot.Space do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Typebot.PrismaDateTime, autogenerate: {Typebot.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          name: String.t(),
          icon: String.t() | nil,
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          workspace: Typebot.Workspace.t() | nil,
          typebots: [Typebot.Typebot.t()]
        }

  schema "Space" do
    field(:id, :string, primary_key: true)
    field(:name, :string)
    field(:icon, :string)
    field(:workspace_id, :string, source: :workspaceId)
    belongs_to(:workspace, Typebot.Workspace, foreign_key: :workspace_id, define_field: false, type: :string)
    has_many(:typebots, Typebot.Typebot, foreign_key: :space_id, references: :id)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end