defmodule InboxZero.GroupItem do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: InboxZero.PrismaDateTime, autogenerate: {InboxZero.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          type: atom(),
          value: String.t(),
          exclude: boolean(),
          reason: String.t() | nil,
          thread_id: String.t() | nil,
          message_id: String.t() | nil,
          source: atom() | nil,
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          group: InboxZero.Group.t() | nil
        }

  schema "GroupItem" do
    field(:id, :string, primary_key: true)
    field(:type, Ecto.Enum, values: [:FROM, :SUBJECT, :BODY])
    field(:value, :string)
    field(:exclude, :boolean, default: false)
    field(:reason, :string)
    field(:thread_id, :string, source: :threadId)
    field(:message_id, :string, source: :messageId)
    field(:source, Ecto.Enum, values: [:AI, :USER, :LABEL_REMOVED, :LABEL_ADDED])
    field(:group_id, :string, source: :groupId)
    belongs_to(:group, InboxZero.Group, foreign_key: :group_id, define_field: false, type: :string)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end