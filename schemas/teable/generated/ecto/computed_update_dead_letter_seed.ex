defmodule Teable.ComputedUpdateDeadLetterSeed do
  use Ecto.Schema

  @primary_key false

  @type t :: %__MODULE__{
          task_id: String.t(),
          table_id: String.t(),
          record_id: String.t(),
          task: Teable.ComputedUpdateDeadLetter.t() | nil
        }

  schema "computed_update_dead_letter_seed" do
    field(:table_id, :string, primary_key: true)
    field(:record_id, :string, primary_key: true)
    field(:task_id, :string, primary_key: true)
    belongs_to(:task, Teable.ComputedUpdateDeadLetter, foreign_key: :task_id, define_field: false, type: :string)
  end
end