defmodule LiteLLM.LiteLLMCredentialsTable do
  use Ecto.Schema

  @primary_key {:id, :binary_id, autogenerate: true, source: :credential_id}
  @foreign_key_type :binary_id
  @timestamps_opts [type: LiteLLM.PrismaDateTime, autogenerate: {LiteLLM.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: Ecto.UUID.t(),
          credential_name: String.t(),
          display_name: String.t() | nil,
          credential_values: map(),
          credential_info: map() | nil,
          created_by: String.t(),
          updated_by: String.t(),
          inserted_at: DateTime.t(),
          updated_at: DateTime.t()
        }

  schema "LiteLLM_CredentialsTable" do
    field(:credential_name, :string)
    field(:display_name, :string)
    field(:credential_values, :map)
    field(:credential_info, :map)
    field(:created_by, :string)
    field(:updated_by, :string)
    timestamps(inserted_at_source: :created_at)
  end
end