defmodule InboxZero.McpTool do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: InboxZero.PrismaDateTime, autogenerate: {InboxZero.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          name: String.t(),
          description: String.t() | nil,
          schema: map() | nil,
          is_enabled: boolean(),
          is_write: boolean(),
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          connection: InboxZero.McpConnection.t() | nil
        }

  schema "McpTool" do
    field(:id, :string, primary_key: true)
    field(:name, :string)
    field(:description, :string)
    field(:schema, :map)
    field(:is_enabled, :boolean, default: true, source: :isEnabled)
    field(:is_write, :boolean, default: false, source: :isWrite)
    field(:connection_id, :string, source: :connectionId)
    belongs_to(:connection, InboxZero.McpConnection, foreign_key: :connection_id, define_field: false, type: :string)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end