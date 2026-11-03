defmodule Teable.ComputedUpdateChangeFrontier do
  use Ecto.Schema

  @primary_key false

  @type t :: %__MODULE__{
          scope_id: String.t(),
          kind: String.t(),
          table_id: String.t(),
          record_id: String.t(),
          field_id: String.t()
        }

  schema "computed_update_change_frontier" do
    field(:scope_id, :string, primary_key: true)
    field(:kind, :string, primary_key: true)
    field(:table_id, :string, primary_key: true)
    field(:record_id, :string, primary_key: true)
    field(:field_id, :string, primary_key: true)
  end
end