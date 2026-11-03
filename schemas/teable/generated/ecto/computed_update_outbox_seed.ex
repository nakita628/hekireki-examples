defmodule Teable.ComputedUpdateOutboxSeed do
  use Ecto.Schema

  @primary_key false

  @type t :: %__MODULE__{
          id: String.t(),
          table_id: String.t(),
          record_id: String.t(),
          task: Teable.ComputedUpdateOutbox.t() | nil
        }

  schema "computed_update_outbox_seed" do
    field(:id, :string, primary_key: true)
    field(:table_id, :string)
    field(:record_id, :string)
    belongs_to(:task, Teable.ComputedUpdateOutbox, foreign_key: :task_id, type: :string)
  end
end