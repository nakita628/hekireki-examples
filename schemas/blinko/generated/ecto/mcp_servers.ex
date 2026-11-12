defmodule Blinko.McpServers do
  use Ecto.Schema

  @primary_key {:id, :id, autogenerate: true}
  @timestamps_opts [type: Blinko.PrismaDateTime, autogenerate: {Blinko.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: integer(),
          name: String.t(),
          description: String.t() | nil,
          type: String.t(),
          command: String.t() | nil,
          args: map() | nil,
          url: String.t() | nil,
          env: map() | nil,
          headers: map() | nil,
          is_enabled: boolean(),
          inserted_at: DateTime.t(),
          updated_at: DateTime.t()
        }

  schema "mcpServers" do
    field(:name, :string)
    field(:description, :string)
    field(:type, :string)
    field(:command, :string)
    field(:args, :map)
    field(:url, :string)
    field(:env, :map)
    field(:headers, :map)
    field(:is_enabled, :boolean, default: true, source: :isEnabled)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end