defmodule Teable.ComputedUpdatePauseScope do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Teable.PrismaDateTime, autogenerate: {Teable.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          scope_type: String.t(),
          scope_id: String.t(),
          paused_at: DateTime.t(),
          paused_by: String.t() | nil,
          resume_at: DateTime.t() | nil,
          reason: String.t() | nil,
          write_policy: String.t(),
          updated_by: String.t() | nil,
          updated_at: DateTime.t()
        }

  schema "computed_update_pause_scope" do
    field(:id, :string, primary_key: true)
    field(:scope_type, :string)
    field(:scope_id, :string)
    field(:paused_at, Teable.PrismaDateTime, autogenerate: true)
    field(:paused_by, :string)
    field(:resume_at, Teable.PrismaDateTime)
    field(:reason, :string)
    field(:write_policy, :string, default: "allow_bounded")
    field(:updated_by, :string)
    timestamps(inserted_at: false)
  end
end