defmodule Formbricks.FeedbackSource do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Formbricks.PrismaDateTime, autogenerate: {Formbricks.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          name: String.t(),
          type: atom(),
          status: atom(),
          import_mode: atom(),
          element_scope: atom(),
          last_sync_at: DateTime.t() | nil,
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          workspace: Formbricks.Workspace.t() | nil,
          feedback_directory: Formbricks.FeedbackDirectory.t() | nil,
          creator: Formbricks.User.t() | nil
        }

  schema "FeedbackSource" do
    field(:id, :string, primary_key: true)
    field(:name, :string)
    field(:type, Ecto.Enum, values: [:formbricks_survey, :csv])
    field(:status, Ecto.Enum, values: [:active, :paused, :error], default: :active)
    field(:import_mode, Ecto.Enum, values: [:completedOnly, :all], default: :completedOnly)
    field(:element_scope, Ecto.Enum, values: [:all, :specific], default: :specific, source: :elementScope)
    field(:last_sync_at, Formbricks.PrismaDateTime)
    field(:workspace_id, :string, source: :workspaceId)
    field(:feedback_directory_id, :string, source: :feedbackDirectoryId)
    belongs_to(:workspace, Formbricks.Workspace, foreign_key: :workspace_id, define_field: false, type: :string)
    belongs_to(:feedback_directory, Formbricks.FeedbackDirectory, foreign_key: :feedback_directory_id, define_field: false, type: :string)
    belongs_to(:creator, Formbricks.User, foreign_key: :created_by, type: :string)
    timestamps(inserted_at_source: :created_at)
  end
end