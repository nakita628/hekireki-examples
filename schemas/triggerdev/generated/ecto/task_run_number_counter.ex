defmodule Trigger.TaskRunNumberCounter do
  use Ecto.Schema

  @primary_key false

  @type t :: %__MODULE__{
          id: String.t(),
          task_identifier: String.t(),
          last_number: integer(),
          environment: Trigger.RuntimeEnvironment.t() | nil
        }

  schema "TaskRunNumberCounter" do
    field(:id, :string, primary_key: true)
    field(:task_identifier, :string, source: :taskIdentifier)
    field(:last_number, :integer, default: 0, source: :lastNumber)
    field(:environment_id, :string, source: :environmentId)
    belongs_to(:environment, Trigger.RuntimeEnvironment, foreign_key: :environment_id, define_field: false, type: :string)
  end
end