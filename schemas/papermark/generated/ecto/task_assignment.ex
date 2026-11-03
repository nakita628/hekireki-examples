defmodule Papermark.TaskAssignment do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Papermark.PrismaDateTime, autogenerate: {Papermark.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          email: String.t() | nil,
          notified_at: DateTime.t() | nil,
          last_reminder_at: DateTime.t() | nil,
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          task: Papermark.Task.t() | nil,
          viewer: Papermark.Viewer.t() | nil,
          group: Papermark.ViewerGroup.t() | nil,
          link: Papermark.Link.t() | nil
        }

  schema "TaskAssignment" do
    field(:id, :string, primary_key: true)
    field(:email, :string)
    field(:notified_at, Papermark.PrismaDateTime, source: :notifiedAt)
    field(:last_reminder_at, Papermark.PrismaDateTime, source: :lastReminderAt)
    field(:task_id, :string, source: :taskId)
    field(:viewer_id, :string, source: :viewerId)
    field(:group_id, :string, source: :groupId)
    field(:link_id, :string, source: :linkId)
    belongs_to(:task, Papermark.Task, foreign_key: :task_id, define_field: false, type: :string)
    belongs_to(:viewer, Papermark.Viewer, foreign_key: :viewer_id, define_field: false, type: :string)
    belongs_to(:group, Papermark.ViewerGroup, foreign_key: :group_id, define_field: false, type: :string)
    belongs_to(:link, Papermark.Link, foreign_key: :link_id, define_field: false, type: :string)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end