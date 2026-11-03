defmodule Langfuse.SsoConfig do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Langfuse.PrismaDateTime, autogenerate: {Langfuse.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          domain: String.t(),
          auth_provider: String.t(),
          auth_config: map() | nil,
          inserted_at: DateTime.t(),
          updated_at: DateTime.t()
        }

  schema "sso_configs" do
    field(:domain, :string, primary_key: true)
    field(:auth_provider, :string)
    field(:auth_config, :map)
    timestamps(inserted_at_source: :created_at)
  end
end