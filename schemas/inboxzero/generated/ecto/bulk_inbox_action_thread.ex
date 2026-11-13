defmodule InboxZero.BulkInboxActionThread do
  use Ecto.Schema

  @primary_key false

  @type t :: %__MODULE__{
          job_id: String.t(),
          thread_id: String.t(),
          job: InboxZero.BulkInboxActionJob.t() | nil
        }

  schema "BulkInboxActionThread" do
    field(:thread_id, :string, primary_key: true, source: :threadId)
    field(:job_id, :string, primary_key: true, source: :jobId)
    belongs_to(:job, InboxZero.BulkInboxActionJob, foreign_key: :job_id, define_field: false, type: :string)
  end
end