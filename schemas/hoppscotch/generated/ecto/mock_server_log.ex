defmodule Hoppscotch.MockServerLog do
  use Ecto.Schema

  @primary_key false

  @type t :: %__MODULE__{
          id: String.t(),
          request_method: String.t(),
          request_path: String.t(),
          request_headers: map(),
          request_body: map() | nil,
          request_query: map() | nil,
          response_status: integer(),
          response_headers: map(),
          response_body: map() | nil,
          response_time: integer(),
          ip_address: String.t() | nil,
          user_agent: String.t() | nil,
          executed_at: DateTime.t(),
          mock_server: Hoppscotch.MockServer.t() | nil
        }

  schema "MockServerLog" do
    field(:id, :string, primary_key: true)
    field(:request_method, :string, source: :requestMethod)
    field(:request_path, :string, source: :requestPath)
    field(:request_headers, :map, source: :requestHeaders)
    field(:request_body, :map, source: :requestBody)
    field(:request_query, :map, source: :requestQuery)
    field(:response_status, :integer, source: :responseStatus)
    field(:response_headers, :map, source: :responseHeaders)
    field(:response_body, :map, source: :responseBody)
    field(:response_time, :integer, source: :responseTime)
    field(:ip_address, :string, source: :ipAddress)
    field(:user_agent, :string, source: :userAgent)
    field(:executed_at, Hoppscotch.PrismaDateTime, autogenerate: true, source: :executedAt)
    field(:mock_server_id, :string, source: :mockServerID)
    belongs_to(:mock_server, Hoppscotch.MockServer, foreign_key: :mock_server_id, define_field: false, type: :string)
  end
end