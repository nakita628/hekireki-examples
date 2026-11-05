defmodule OpenPanel.CodeMigration do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: OpenPanel.PrismaDateTime, autogenerate: {OpenPanel.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          name: String.t(),
          inserted_at: DateTime.t(),
          updated_at: DateTime.t()
        }

  schema "__code_migrations" do
    field(:id, :string, primary_key: true, read_after_writes: true)
    field(:name, :string)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end