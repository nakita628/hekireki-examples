defmodule Plunk.ApiRequest do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Plunk.PrismaDateTime, autogenerate: {Plunk.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          method: String.t(),
          path: String.t(),
          status_code: integer(),
          duration: integer(),
          user_id: String.t() | nil,
          auth_type: String.t() | nil,
          ip: String.t() | nil,
          user_agent: String.t() | nil,
          error_code: String.t() | nil,
          error_message: String.t() | nil,
          request_size: integer() | nil,
          response_size: integer() | nil,
          inserted_at: DateTime.t(),
          project: Plunk.Project.t() | nil
        }

  schema "api_requests" do
    field(:id, :string, primary_key: true)
    field(:method, :string)
    field(:path, :string)
    field(:status_code, :integer, source: :statusCode)
    field(:duration, :integer)
    field(:user_id, :string, source: :userId)
    field(:auth_type, :string, source: :authType)
    field(:ip, :string)
    field(:user_agent, :string, source: :userAgent)
    field(:error_code, :string, source: :errorCode)
    field(:error_message, :string, source: :errorMessage)
    field(:request_size, :integer, source: :requestSize)
    field(:response_size, :integer, source: :responseSize)
    field(:project_id, :binary_id, source: :projectId)
    belongs_to(:project, Plunk.Project, foreign_key: :project_id, define_field: false, type: :binary_id)
    timestamps(inserted_at_source: :createdAt, updated_at: false)
  end
end