defmodule Blinko.AiScheduledTask do
  use Ecto.Schema

  @primary_key {:id, :id, autogenerate: true}
  @timestamps_opts [type: Blinko.PrismaDateTime, autogenerate: {Blinko.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: integer(),
          name: String.t(),
          prompt: String.t(),
          schedule: String.t(),
          is_enabled: boolean(),
          last_run: DateTime.t() | nil,
          last_result: map() | nil,
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          account: Blinko.Accounts.t() | nil
        }

  schema "aiScheduledTask" do
    field(:name, :string)
    field(:prompt, :string)
    field(:schedule, :string)
    field(:is_enabled, :boolean, default: true, source: :isEnabled)
    field(:last_run, Blinko.PrismaDateTime, source: :lastRun)
    field(:last_result, :map, source: :lastResult)
    field(:account_id, :id, source: :accountId)
    belongs_to(:account, Blinko.Accounts, foreign_key: :account_id, define_field: false)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end