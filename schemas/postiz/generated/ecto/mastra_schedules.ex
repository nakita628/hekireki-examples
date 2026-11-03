defmodule Postiz.MastraSchedules do
  use Ecto.Schema

  @primary_key false

  @type t :: %__MODULE__{
          id: String.t(),
          target: map(),
          cron: String.t(),
          timezone: String.t() | nil,
          status: String.t(),
          next_fire_at: integer(),
          last_fire_at: integer() | nil,
          last_run_id: String.t() | nil,
          created_at: integer(),
          updated_at: integer(),
          metadata: map() | nil,
          owner_type: String.t() | nil,
          owner_id: String.t() | nil
        }

  schema "mastra_schedules" do
    field(:id, :string, primary_key: true)
    field(:target, :map)
    field(:cron, :string)
    field(:timezone, :string)
    field(:status, :string)
    field(:next_fire_at, :integer)
    field(:last_fire_at, :integer)
    field(:last_run_id, :string)
    field(:created_at, :integer)
    field(:updated_at, :integer)
    field(:metadata, :map)
    field(:owner_type, :string)
    field(:owner_id, :string)
  end
end