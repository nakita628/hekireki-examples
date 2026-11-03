defmodule Trigger.SessionRun do
  use Ecto.Schema

  @primary_key false

  @type t :: %__MODULE__{
          id: String.t(),
          run_id: String.t(),
          reason: String.t(),
          triggered_at: DateTime.t(),
          session: Trigger.Session.t() | nil
        }

  schema "SessionRun" do
    field(:id, :string, primary_key: true)
    field(:run_id, :string, source: :runId)
    field(:reason, :string)
    field(:triggered_at, Trigger.PrismaDateTime, autogenerate: true, source: :triggeredAt)
    field(:session_id, :string, source: :sessionId)
    belongs_to(:session, Trigger.Session, foreign_key: :session_id, define_field: false, type: :string)
  end
end