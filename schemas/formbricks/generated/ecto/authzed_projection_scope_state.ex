defmodule Formbricks.AuthzedProjectionScopeState do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Formbricks.PrismaDateTime, autogenerate: {Formbricks.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          scope: String.t(),
          ready_at: DateTime.t() | nil,
          ready_by: String.t() | nil,
          updated_at: DateTime.t()
        }

  schema "AuthzedProjectionScopeState" do
    field(:scope, :string, primary_key: true)
    field(:ready_at, Formbricks.PrismaDateTime, source: :readyAt)
    field(:ready_by, :string, source: :readyBy)
    timestamps(inserted_at: false, updated_at_source: :updatedAt)
  end
end