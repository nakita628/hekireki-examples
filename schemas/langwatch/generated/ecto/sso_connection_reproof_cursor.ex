defmodule LangWatch.SsoConnectionReproofCursor do
  use Ecto.Schema

  @primary_key false

  @type t :: %__MODULE__{
          last_reproof_at: DateTime.t(),
          connection: LangWatch.SsoConnection.t() | nil
        }

  schema "SsoConnectionReproofCursor" do
    field(:last_reproof_at, LangWatch.PrismaDateTime, source: :lastReproofAt)
    field(:connection_id, :string, source: :connectionId)
    belongs_to(:connection, LangWatch.SsoConnection, foreign_key: :connection_id, define_field: false, type: :string)
  end
end