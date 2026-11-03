defmodule Teable.ComputedUpdateStageLedger do
  use Ecto.Schema

  @primary_key false

  @type t :: %__MODULE__{
          scope_id: String.t(),
          kind: String.t(),
          table_id: String.t(),
          record_id: String.t(),
          seq: integer()
        }

  schema "computed_update_stage_ledger" do
    field(:scope_id, :string, primary_key: true)
    field(:kind, :string, primary_key: true)
    field(:table_id, :string, primary_key: true)
    field(:record_id, :string, primary_key: true)
    field(:seq, :integer, default: 0)
  end
end