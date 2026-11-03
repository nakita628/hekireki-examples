defmodule Postiz.MastraScheduleTriggers do
  use Ecto.Schema

  @primary_key false

  @type t :: %__MODULE__{
          id: String.t(),
          schedule_id: String.t(),
          run_id: String.t() | nil,
          scheduled_fire_at: integer(),
          actual_fire_at: integer(),
          outcome: String.t(),
          error: String.t() | nil,
          trigger_kind: String.t(),
          parent_trigger_id: String.t() | nil,
          metadata: map() | nil
        }

  schema "mastra_schedule_triggers" do
    field(:id, :string, primary_key: true)
    field(:schedule_id, :string)
    field(:run_id, :string)
    field(:scheduled_fire_at, :integer)
    field(:actual_fire_at, :integer)
    field(:outcome, :string)
    field(:error, :string)
    field(:trigger_kind, :string)
    field(:parent_trigger_id, :string)
    field(:metadata, :map)
  end
end