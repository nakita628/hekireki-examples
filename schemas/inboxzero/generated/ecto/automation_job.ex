defmodule InboxZero.AutomationJob do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: InboxZero.PrismaDateTime, autogenerate: {InboxZero.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          name: String.t(),
          enabled: boolean(),
          prompt: String.t() | nil,
          cron_expression: String.t(),
          next_run_at: DateTime.t(),
          messaging_channel_id: String.t(),
          email_account_id: String.t(),
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          runs: [InboxZero.AutomationJobRun.t()]
        }

  schema "AutomationJob" do
    field(:id, :string, primary_key: true)
    field(:name, :string)
    field(:enabled, :boolean, default: true)
    field(:prompt, :string)
    field(:cron_expression, :string, source: :cronExpression)
    field(:next_run_at, InboxZero.PrismaDateTime, source: :nextRunAt)
    field(:messaging_channel_id, :string, source: :messagingChannelId)
    field(:email_account_id, :string, source: :emailAccountId)
    has_many(:runs, InboxZero.AutomationJobRun, foreign_key: :automation_job_id, references: :id)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end