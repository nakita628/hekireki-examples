defmodule InboxZero.CleanupThread do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: InboxZero.PrismaDateTime, autogenerate: {InboxZero.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          thread_id: String.t(),
          archived: boolean(),
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          job: InboxZero.CleanupJob.t() | nil,
          email_account: InboxZero.EmailAccount.t() | nil
        }

  schema "CleanupThread" do
    field(:id, :string, primary_key: true)
    field(:thread_id, :string, source: :threadId)
    field(:archived, :boolean)
    field(:job_id, :string, source: :jobId)
    field(:email_account_id, :string, source: :emailAccountId)
    belongs_to(:job, InboxZero.CleanupJob, foreign_key: :job_id, define_field: false, type: :string)
    belongs_to(:email_account, InboxZero.EmailAccount, foreign_key: :email_account_id, define_field: false, type: :string)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end