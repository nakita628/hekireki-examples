defmodule Postiz.MastraMessages do
  use Ecto.Schema

  @primary_key false

  @type t :: %__MODULE__{
          id: String.t(),
          thread_id: String.t(),
          content: String.t(),
          role: String.t(),
          type: String.t(),
          created_at: DateTime.t(),
          resource_id: String.t() | nil,
          created_at_z: DateTime.t() | nil
        }

  schema "mastra_messages" do
    field(:id, :string, primary_key: true)
    field(:thread_id, :string)
    field(:content, :string)
    field(:role, :string)
    field(:type, :string)
    field(:created_at, Postiz.PrismaDateTime, source: :createdAt)
    field(:resource_id, :string, source: :resourceId)
    field(:created_at_z, Postiz.PrismaDateTime, autogenerate: true, source: :createdAtZ)
  end
end