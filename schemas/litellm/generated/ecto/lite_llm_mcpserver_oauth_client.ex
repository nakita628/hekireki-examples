defmodule LiteLLM.LiteLLMMCPServerOAuthClient do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: LiteLLM.PrismaDateTime, autogenerate: {LiteLLM.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          server_id: String.t(),
          credentials: map() | nil,
          inserted_at: DateTime.t(),
          updated_at: DateTime.t()
        }

  schema "LiteLLM_MCPServerOAuthClient" do
    field(:server_id, :string, primary_key: true)
    field(:credentials, :map)
    timestamps(inserted_at_source: :created_at)
  end
end