defmodule Papermark.TaskActivity do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Papermark.PrismaDateTime, autogenerate: {Papermark.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          type: String.t(),
          comment: String.t() | nil,
          from_status: String.t() | nil,
          to_status: String.t() | nil,
          inserted_at: DateTime.t(),
          task: Papermark.Task.t() | nil,
          user: Papermark.User.t() | nil,
          viewer: Papermark.Viewer.t() | nil,
          view: Papermark.View.t() | nil
        }

  schema "TaskActivity" do
    field(:id, :string, primary_key: true)
    field(:type, :string)
    field(:comment, :string)
    field(:from_status, :string, source: :fromStatus)
    field(:to_status, :string, source: :toStatus)
    field(:task_id, :string, source: :taskId)
    field(:user_id, :string, source: :userId)
    field(:viewer_id, :string, source: :viewerId)
    field(:view_id, :string, source: :viewId)
    belongs_to(:task, Papermark.Task, foreign_key: :task_id, define_field: false, type: :string)
    belongs_to(:user, Papermark.User, foreign_key: :user_id, define_field: false, type: :string)
    belongs_to(:viewer, Papermark.Viewer, foreign_key: :viewer_id, define_field: false, type: :string)
    belongs_to(:view, Papermark.View, foreign_key: :view_id, define_field: false, type: :string)
    timestamps(inserted_at_source: :createdAt, updated_at: false)
  end
end