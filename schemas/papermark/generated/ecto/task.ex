defmodule Papermark.Task do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Papermark.PrismaDateTime, autogenerate: {Papermark.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          title: String.t(),
          description: String.t() | nil,
          type: String.t(),
          status: String.t(),
          category: String.t() | nil,
          due_date: DateTime.t() | nil,
          order_index: integer() | nil,
          last_activity_notified_at: DateTime.t() | nil,
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          task_list: Papermark.TaskList.t() | nil,
          dataroom: Papermark.Dataroom.t() | nil,
          team: Papermark.Team.t() | nil,
          upload_folder: Papermark.DataroomFolder.t() | nil,
          created_by_user: Papermark.User.t() | nil,
          assignments: [Papermark.TaskAssignment.t()],
          activities: [Papermark.TaskActivity.t()],
          uploads: [Papermark.DocumentUpload.t()]
        }

  schema "Task" do
    field(:id, :string, primary_key: true)
    field(:title, :string)
    field(:description, :string)
    field(:type, :string, default: "TODO")
    field(:status, :string, default: "OPEN")
    field(:category, :string)
    field(:due_date, Papermark.PrismaDateTime, source: :dueDate)
    field(:order_index, :integer, source: :orderIndex)
    field(:last_activity_notified_at, Papermark.PrismaDateTime, source: :lastActivityNotifiedAt)
    field(:task_list_id, :string, source: :taskListId)
    field(:dataroom_id, :string, source: :dataroomId)
    field(:team_id, :string, source: :teamId)
    field(:upload_folder_id, :string, source: :uploadFolderId)
    field(:created_by_user_id, :string, source: :createdByUserId)
    belongs_to(:task_list, Papermark.TaskList, foreign_key: :task_list_id, define_field: false, type: :string)
    belongs_to(:dataroom, Papermark.Dataroom, foreign_key: :dataroom_id, define_field: false, type: :string)
    belongs_to(:team, Papermark.Team, foreign_key: :team_id, define_field: false, type: :string)
    belongs_to(:upload_folder, Papermark.DataroomFolder, foreign_key: :upload_folder_id, define_field: false, type: :string)
    belongs_to(:created_by_user, Papermark.User, foreign_key: :created_by_user_id, define_field: false, type: :string)
    has_many(:assignments, Papermark.TaskAssignment, foreign_key: :task_id)
    has_many(:activities, Papermark.TaskActivity, foreign_key: :task_id)
    has_many(:uploads, Papermark.DocumentUpload, foreign_key: :task_id)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end