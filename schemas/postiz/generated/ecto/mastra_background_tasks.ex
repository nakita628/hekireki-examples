defmodule Postiz.MastraBackgroundTasks do
  use Ecto.Schema

  @primary_key false

  @type t :: %__MODULE__{
          id: String.t(),
          tool_call_id: String.t(),
          tool_name: String.t(),
          agent_id: String.t(),
          run_id: String.t(),
          thread_id: String.t() | nil,
          resource_id: String.t() | nil,
          status: String.t(),
          args: map(),
          result: map() | nil,
          error: map() | nil,
          suspend_payload: map() | nil,
          retry_count: integer(),
          max_retries: integer(),
          timeout_ms: integer(),
          created_at: DateTime.t(),
          started_at: DateTime.t() | nil,
          suspended_at: DateTime.t() | nil,
          completed_at: DateTime.t() | nil,
          created_at_z: DateTime.t() | nil,
          started_at_z: DateTime.t() | nil,
          suspended_at_z: DateTime.t() | nil,
          completed_at_z: DateTime.t() | nil
        }

  schema "mastra_background_tasks" do
    field(:id, :string, primary_key: true)
    field(:tool_call_id, :string)
    field(:tool_name, :string)
    field(:agent_id, :string)
    field(:run_id, :string)
    field(:thread_id, :string)
    field(:resource_id, :string)
    field(:status, :string)
    field(:args, :map)
    field(:result, :map)
    field(:error, :map)
    field(:suspend_payload, :map)
    field(:retry_count, :integer)
    field(:max_retries, :integer)
    field(:timeout_ms, :integer)
    field(:created_at, Postiz.PrismaDateTime, source: :createdAt)
    field(:started_at, Postiz.PrismaDateTime, source: :startedAt)
    field(:suspended_at, Postiz.PrismaDateTime, source: :suspendedAt)
    field(:completed_at, Postiz.PrismaDateTime, source: :completedAt)
    field(:created_at_z, Postiz.PrismaDateTime, autogenerate: true, source: :createdAtZ)
    field(:started_at_z, Postiz.PrismaDateTime, autogenerate: true, source: :startedAtZ)
    field(:suspended_at_z, Postiz.PrismaDateTime, autogenerate: true, source: :suspendedAtZ)
    field(:completed_at_z, Postiz.PrismaDateTime, autogenerate: true, source: :completedAtZ)
  end
end