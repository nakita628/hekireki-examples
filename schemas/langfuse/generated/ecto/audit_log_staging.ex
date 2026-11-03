defmodule Langfuse.AuditLogStaging do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Langfuse.PrismaDateTime, autogenerate: {Langfuse.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          payload: map(),
          failed_at: DateTime.t() | nil,
          last_error: String.t() | nil,
          inserted_at: DateTime.t()
        }

  schema "audit_log_staging" do
    field(:id, :string, primary_key: true)
    field(:payload, :map)
    field(:failed_at, Langfuse.PrismaDateTime)
    field(:last_error, :string)
    timestamps(inserted_at_source: :created_at, updated_at: false)
  end
end