defmodule Typebot.DashboardFolder do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Typebot.PrismaDateTime, autogenerate: {Typebot.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          name: String.t(),
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          parent_folder: Typebot.DashboardFolder.t() | nil,
          workspace: Typebot.Workspace.t() | nil,
          children_folder: [Typebot.DashboardFolder.t()],
          typebots: [Typebot.Typebot.t()]
        }

  schema "DashboardFolder" do
    field(:id, :string, primary_key: true)
    field(:name, :string)
    field(:parent_folder_id, :string, source: :parentFolderId)
    field(:workspace_id, :string, source: :workspaceId)
    belongs_to(:parent_folder, Typebot.DashboardFolder, foreign_key: :parent_folder_id, define_field: false, type: :string)
    belongs_to(:workspace, Typebot.Workspace, foreign_key: :workspace_id, define_field: false, type: :string)
    has_many(:children_folder, Typebot.DashboardFolder, foreign_key: :parent_folder_id)
    has_many(:typebots, Typebot.Typebot, foreign_key: :folder_id)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end