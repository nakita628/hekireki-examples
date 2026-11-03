defmodule Papermark.ViewerInvitation do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Papermark.PrismaDateTime, autogenerate: {Papermark.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          invited_by: String.t(),
          custom_message: String.t() | nil,
          sent_at: DateTime.t(),
          status: atom(),
          inserted_at: DateTime.t(),
          viewer: Papermark.Viewer.t() | nil,
          link: Papermark.Link.t() | nil,
          group: Papermark.ViewerGroup.t() | nil
        }

  schema "ViewerInvitation" do
    field(:id, :string, primary_key: true)
    field(:invited_by, :string, source: :invitedBy)
    field(:custom_message, :string, source: :customMessage)
    field(:sent_at, Papermark.PrismaDateTime, autogenerate: true, source: :sentAt)
    field(:status, Ecto.Enum, values: [:SENT, :FAILED, :BOUNCED], default: :SENT)
    field(:viewer_id, :string, source: :viewerId)
    field(:link_id, :string, source: :linkId)
    field(:group_id, :string, source: :groupId)
    belongs_to(:viewer, Papermark.Viewer, foreign_key: :viewer_id, define_field: false, type: :string)
    belongs_to(:link, Papermark.Link, foreign_key: :link_id, define_field: false, type: :string)
    belongs_to(:group, Papermark.ViewerGroup, foreign_key: :group_id, define_field: false, type: :string)
    timestamps(inserted_at_source: :createdAt, updated_at: false)
  end
end