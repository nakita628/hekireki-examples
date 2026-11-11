defmodule LiteLLM.LiteLLMHealthCheckTable do
  use Ecto.Schema

  @primary_key {:id, :binary_id, autogenerate: true, source: :health_check_id}
  @foreign_key_type :binary_id
  @timestamps_opts [type: LiteLLM.PrismaDateTime, autogenerate: {LiteLLM.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: Ecto.UUID.t(),
          model_name: String.t(),
          model_id: String.t() | nil,
          status: String.t(),
          healthy_count: integer(),
          unhealthy_count: integer(),
          error_message: String.t() | nil,
          response_time_ms: float() | nil,
          details: map() | nil,
          checked_by: String.t() | nil,
          checked_at: DateTime.t(),
          inserted_at: DateTime.t(),
          updated_at: DateTime.t()
        }

  schema "LiteLLM_HealthCheckTable" do
    field(:model_name, :string)
    field(:model_id, :string)
    field(:status, :string)
    field(:healthy_count, :integer, default: 0)
    field(:unhealthy_count, :integer, default: 0)
    field(:error_message, :string)
    field(:response_time_ms, :float)
    field(:details, :map)
    field(:checked_by, :string)
    field(:checked_at, LiteLLM.PrismaDateTime, autogenerate: true)
    timestamps(inserted_at_source: :created_at)
  end
end