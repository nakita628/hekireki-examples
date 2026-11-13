defmodule LiteLLM.LiteLLMScheduledJobRun do
  use Ecto.Schema

  @primary_key {:id, :binary_id, autogenerate: true}
  @foreign_key_type :binary_id
  @timestamps_opts [type: LiteLLM.PrismaDateTime, autogenerate: {LiteLLM.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: Ecto.UUID.t(),
          job_name: String.t(),
          lock_group: String.t(),
          schedule: String.t(),
          scheduled_for: DateTime.t(),
          status: atom(),
          claimed_by: String.t() | nil,
          claimed_at: DateTime.t() | nil,
          lease_expires_at: DateTime.t() | nil,
          finished_at: DateTime.t() | nil,
          inserted_at: DateTime.t()
        }

  schema "LiteLLM_ScheduledJobRun" do
    field(:job_name, :string)
    field(:lock_group, :string)
    field(:schedule, :string)
    field(:scheduled_for, LiteLLM.PrismaDateTime)
    field(:status, Ecto.Enum, values: [:PENDING, :RUNNING, :SUCCEEDED, :FAILED, :CANCELLED], default: :PENDING)
    field(:claimed_by, :string)
    field(:claimed_at, LiteLLM.PrismaDateTime)
    field(:lease_expires_at, LiteLLM.PrismaDateTime)
    field(:finished_at, LiteLLM.PrismaDateTime)
    timestamps(inserted_at_source: :created_at, updated_at: false)
  end
end