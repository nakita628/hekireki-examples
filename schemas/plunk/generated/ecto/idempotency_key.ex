defmodule Plunk.IdempotencyKey do
  use Ecto.Schema

  @primary_key {:id, :binary_id, autogenerate: true}
  @foreign_key_type :binary_id
  @timestamps_opts [type: Plunk.PrismaDateTime, autogenerate: {Plunk.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: Ecto.UUID.t(),
          key: String.t(),
          method: String.t(),
          path: String.t(),
          status_code: integer() | nil,
          expires_at: DateTime.t(),
          inserted_at: DateTime.t(),
          project: Plunk.Project.t() | nil
        }

  schema "idempotency_keys" do
    field(:key, :string)
    field(:method, :string)
    field(:path, :string)
    field(:status_code, :integer, source: :statusCode)
    field(:expires_at, Plunk.PrismaDateTime, source: :expiresAt)
    field(:project_id, :binary_id, source: :projectId)
    belongs_to(:project, Plunk.Project, foreign_key: :project_id, define_field: false)
    timestamps(inserted_at_source: :createdAt, updated_at: false)
  end
end