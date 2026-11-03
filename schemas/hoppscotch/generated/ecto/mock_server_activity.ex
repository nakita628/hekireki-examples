defmodule Hoppscotch.MockServerActivity do
  use Ecto.Schema

  @primary_key false

  @type t :: %__MODULE__{
          id: String.t(),
          action: atom(),
          performed_by: String.t() | nil,
          performed_at: DateTime.t(),
          mock_server: Hoppscotch.MockServer.t() | nil
        }

  schema "MockServerActivity" do
    field(:id, :string, primary_key: true)
    field(:action, Ecto.Enum, values: [:CREATED, :DELETED, :ACTIVATED, :DEACTIVATED])
    field(:performed_by, :string, source: :performedBy)
    field(:performed_at, Hoppscotch.PrismaDateTime, autogenerate: true, source: :performedAt)
    field(:mock_server_id, :string, source: :mockServerID)
    belongs_to(:mock_server, Hoppscotch.MockServer, foreign_key: :mock_server_id, define_field: false, type: :string)
  end
end