defmodule Formbricks.FeedbackDirectoryWorkspace do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Formbricks.PrismaDateTime, autogenerate: {Formbricks.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          feedback_directory_id: String.t(),
          workspace_id: String.t(),
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          feedback_directory: Formbricks.FeedbackDirectory.t() | nil,
          workspace: Formbricks.Workspace.t() | nil
        }

  schema "FeedbackDirectoryWorkspace" do
    field(:feedback_directory_id, :string, primary_key: true, source: :feedbackDirectoryId)
    field(:workspace_id, :string, primary_key: true, source: :workspaceId)
    belongs_to(:feedback_directory, Formbricks.FeedbackDirectory, foreign_key: :feedback_directory_id, define_field: false, type: :string)
    belongs_to(:workspace, Formbricks.Workspace, foreign_key: :workspace_id, define_field: false, type: :string)
    timestamps(inserted_at_source: :created_at)
  end
end