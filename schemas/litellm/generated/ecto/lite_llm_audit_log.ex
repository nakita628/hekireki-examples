defmodule LiteLLM.LiteLLMAuditLog do
  use Ecto.Schema

  @primary_key {:id, :binary_id, autogenerate: true}
  @foreign_key_type :binary_id

  @type t :: %__MODULE__{
          id: Ecto.UUID.t(),
          updated_at: DateTime.t(),
          changed_by: String.t(),
          changed_by_api_key: String.t(),
          action: String.t(),
          table_name: String.t(),
          object_id: String.t(),
          before_value: map() | nil,
          updated_values: map() | nil
        }

  schema "LiteLLM_AuditLog" do
    field(:updated_at, LiteLLM.PrismaDateTime, autogenerate: true)
    field(:changed_by, :string, default: "")
    field(:changed_by_api_key, :string, default: "")
    field(:action, :string)
    field(:table_name, :string)
    field(:object_id, :string)
    field(:before_value, :map)
    field(:updated_values, :map)
  end
end