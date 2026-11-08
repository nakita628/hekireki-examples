defmodule Plunk.Campaign do
  use Ecto.Schema

  @primary_key {:id, :binary_id, autogenerate: true}
  @foreign_key_type :binary_id
  @timestamps_opts [type: Plunk.PrismaDateTime, autogenerate: {Plunk.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: Ecto.UUID.t(),
          name: String.t(),
          description: String.t() | nil,
          status: atom(),
          subject: String.t(),
          body: String.t(),
          from: String.t(),
          from_name: String.t() | nil,
          reply_to: String.t() | nil,
          type: atom(),
          audience_type: atom(),
          audience_condition: map() | nil,
          scheduled_for: DateTime.t() | nil,
          total_recipients: integer(),
          sent_count: integer(),
          delivered_count: integer(),
          opened_count: integer(),
          clicked_count: integer(),
          bounced_count: integer(),
          complained_count: integer(),
          unsubscribed_count: integer(),
          archived: boolean(),
          sent_at: DateTime.t() | nil,
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          segment: Plunk.Segment.t() | nil,
          project: Plunk.Project.t() | nil,
          emails: [Plunk.Email.t()]
        }

  schema "campaigns" do
    field(:name, :string)
    field(:description, :string)
    field(:status, Ecto.Enum, values: [:DRAFT, :SCHEDULED, :SENDING, :SENT, :CANCELLED], default: :DRAFT)
    field(:subject, :string)
    field(:body, :string)
    field(:from, :string)
    field(:from_name, :string, source: :fromName)
    field(:reply_to, :string, source: :replyTo)
    field(:type, Ecto.Enum, values: [:TRANSACTIONAL, :MARKETING, :HEADLESS], default: :MARKETING)
    field(:audience_type, Ecto.Enum, values: [:ALL, :FILTERED, :SEGMENT], default: :ALL, source: :audienceType)
    field(:audience_condition, :map, source: :audienceCondition)
    field(:scheduled_for, Plunk.PrismaDateTime, source: :scheduledFor)
    field(:total_recipients, :integer, default: 0, source: :totalRecipients)
    field(:sent_count, :integer, default: 0, source: :sentCount)
    field(:delivered_count, :integer, default: 0, source: :deliveredCount)
    field(:opened_count, :integer, default: 0, source: :openedCount)
    field(:clicked_count, :integer, default: 0, source: :clickedCount)
    field(:bounced_count, :integer, default: 0, source: :bouncedCount)
    field(:complained_count, :integer, default: 0, source: :complainedCount)
    field(:unsubscribed_count, :integer, default: 0, source: :unsubscribedCount)
    field(:archived, :boolean, default: false)
    field(:sent_at, Plunk.PrismaDateTime, source: :sentAt)
    field(:segment_id, :binary_id, source: :segmentId)
    field(:project_id, :binary_id, source: :projectId)
    belongs_to(:segment, Plunk.Segment, foreign_key: :segment_id, define_field: false)
    belongs_to(:project, Plunk.Project, foreign_key: :project_id, define_field: false)
    has_many(:emails, Plunk.Email, foreign_key: :campaign_id)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end