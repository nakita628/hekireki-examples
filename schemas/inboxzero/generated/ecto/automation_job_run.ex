defmodule InboxZero.AutomationJobRun do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: InboxZero.PrismaDateTime, autogenerate: {InboxZero.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          status: atom(),
          scheduled_for: DateTime.t(),
          processed_at: DateTime.t() | nil,
          outbound_message: String.t() | nil,
          provider_message_id: String.t() | nil,
          error: String.t() | nil,
          inserted_at: DateTime.t(),
          automation_job: InboxZero.AutomationJob.t() | nil
        }

  schema "AutomationJobRun" do
    field(:id, :string, primary_key: true)
    field(:status, Ecto.Enum, values: [:PENDING, :RUNNING, :SENT, :SKIPPED, :FAILED], default: :PENDING)
    field(:scheduled_for, InboxZero.PrismaDateTime, source: :scheduledFor)
    field(:processed_at, InboxZero.PrismaDateTime, source: :processedAt)
    field(:outbound_message, :string, source: :outboundMessage)
    field(:provider_message_id, :string, source: :providerMessageId)
    field(:error, :string)
    field(:automation_job_id, :string, source: :automationJobId)
    belongs_to(:automation_job, InboxZero.AutomationJob, foreign_key: :automation_job_id, define_field: false, type: :string)
    timestamps(inserted_at_source: :createdAt, updated_at: false)
  end
end